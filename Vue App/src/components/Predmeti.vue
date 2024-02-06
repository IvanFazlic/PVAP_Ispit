<script setup>
import { ref, watch } from 'vue'
import axios from 'axios';
import Predmet from './Predmet.vue'
import PORT from '../assets/PORT'
const toggle = ref(true)
const props = defineProps(["data"])


let dodavanje = ref([])

let predmeti = ref([])
let predmetiZapisnika = ref([])
let predmetiZaDodavanje = ref([])

const dohvatiPredmeteStudenta = async () => {
    
    const sviPredmetiStudenta = await axios.get(`https://localhost:${PORT}/api/StudentPredmets/${props.data.values.idStudenta}`)
    const dohvatiPredmeteZaDodavanje = await axios.get(`https://localhost:7129/api/StudentPredmets/moguceDodati/${props.data.values.idStudenta}`)
    const zapisnikStudenta = await axios.get(`https://localhost:7129/api/Zapisniks/${props.data.values.idStudenta}`)
    predmetiZaDodavanje = dohvatiPredmeteZaDodavanje.data
    predmeti.value = sviPredmetiStudenta.data
    predmetiZapisnika = zapisnikStudenta.data

}
const obrisiPredmet = (idStudenta, idPredmeta) => {
    axios.delete(`https://localhost:${PORT}/api/StudentPredmets/${idStudenta}/${idPredmeta}`)
        .then((res) => {
            dohvatiPredmeteStudenta()
            alert("Uspesno obrisan predmet")
        })
        .catch(err=>{
            alert("Ne mogu taj.")
        })
}

const handlePredmet = (arg, req) => {
    if (req == 'brisanjePredmeta') {
        obrisiPredmet(props.data.values.idStudenta, arg.idPredmeta)
    }
}

watch(props, () => {
    dohvatiPredmeteStudenta()
})

//Napraviti da ucita ponovo
const dodajPredmete = () => {
    if(dodavanje.value.length <= 0){
        alert("Nista dodali predmet.")
        return
    }
    let predmeti = dodavanje.value
    predmeti.forEach(element => {
        let elementZaSlanje = {
            "idStudenta": props.data.values.idStudenta,
            "idPredmeta": element.idPredmeta,
        }
        axios.post(`https://localhost:${PORT}/api/StudentPredmets`, elementZaSlanje)
            .then(() => {
                alert(`Dodat predmet "${element.naziv}" studentu "${props.data.values.ime}"`)
            })
            .finally(() => {
                dohvatiPredmeteStudenta()
                dodavanje.value = []
            })
    });
    dodavanje.value = []
}

</script>

<template>
    <div v-if="props.data.values">
        <h2>Predmeti studenta {{ props.data.values.ime }} {{ props.data.values.prezime }}</h2>
        <table>
            <thead>
                <th>ID predmeta</th>
                <th>ID profesora</th>
                <th>Naziv predmeta</th>
                <th>Broj ESPB</th>
                <th>Status predmeta</th>
            </thead>
            <tbody>
                <Predmet v-for="p in predmeti" :data="p" :zapisnikStudneta="predmetiZapisnika" @handlePredmet="(arg, req) => handlePredmet(arg, req)"
                    :toggle="toggle"></Predmet>
            </tbody>
        </table>
        <br>
        <select name="cars" id="cars" v-model="dodavanje" multiple>
            <option v-for="p in predmetiZaDodavanje" :value="p">{{ p.naziv }}</option>
        </select>
        <br><br>
        <button @click="dodajPredmete" class="dodaj">Dodaj</button>
        <button @click="toggle = !toggle" class="zapisnik">Prikazi zapisnik</button>
    </div>
    <div>

    </div>
</template>

<style scoped>
table,
th,
td {
    border: 1px solid black;
    border-collapse: collapse;
}

table {
    text-align: center;
}

th {
    padding: 0 5px;
}

button {
    margin-right: 10px;
}
</style>