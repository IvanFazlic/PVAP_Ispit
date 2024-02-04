<script setup>
import { ref, watch } from 'vue'
import axios from 'axios';
import Predmet from './Predmet.vue'
import PORT from '../assets/PORT'
const toggle = ref(true)
const props = defineProps(["data"])

let sviPredmeti = ref([])
let predmetiStudenta = ref([])
let ispitiStudenta = ref([])
let zapisnikStudenta = ref([])
let rokovi = ref([])
let dodavanje = ref([])

let predmeti = ref([])
let predmetiZapisnika = ref([])
let predmetiZaDodavanje = ref([])

const dohvatiPredmeteStudenta = async () => {
    
    const sviPredmetiStudenta = await axios.get(`https://localhost:${PORT}/api/StudentPredmets/${props.data.values.idStudenta}`)
    const dohvatiFiltriraneStudente = await axios.get(`https://localhost:${PORT}/api/StudentPredmets`)
    const dohvatiZapisnik = await axios.get(`https://localhost:${PORT}/api/Zapisniks`)
    const dohvatiIspite = await axios.get(`https://localhost:${PORT}/api/Ispits`)

    predmetiStudenta.value = await dohvatiFiltriraneStudente.data.filter(predmet => predmet.idStudenta == props.data.values.idStudenta)
    zapisnikStudenta.value = await dohvatiZapisnik.data.filter(zapisnik => zapisnik.idStudenta == props.data.values.idStudenta)
    ispitiStudenta.value = await dohvatiIspite.data
    sviPredmeti.value = await sviPredmetiStudenta.data

    predmeti.value = sviPredmeti.value.filter(p1 =>
        predmetiStudenta.value.some(p2 => p2.idPredmeta === p1.idPredmeta)
    );
    predmetiZaDodavanje.value = sviPredmeti.value.filter(p1 =>
        !predmetiStudenta.value.some(p2 => p2.idPredmeta === p1.idPredmeta)
    )
    predmetiZapisnika.value = zapisnikStudenta.value.map(obj1 => {
        const matchingObj = ispitiStudenta.value.find(obj2 => obj1.idIspita === obj2.idIspita);
        return { ...obj1, ...matchingObj }
    });

    predmeti.value = predmeti.value.map(obj1 => {
        const matchingObj = predmetiZapisnika.value.find(obj2 => obj1.idPredmeta === obj2.idPredmeta);
        return { ...obj1, ...matchingObj }
    })
}
const obrisiPredmet = (idStudenta, idPredmeta) => {
    axios.delete(`http://pabp.viser.edu.rs:8000/api/StudentPredmets/${idStudenta}/${idPredmeta}`)
        .then((res) => {
            dohvatiPredmeteStudenta()
            alert("Uspesno obrisan predmet")
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
    let predmeti = predmetiZaDodavanje.value.filter(p1 =>
        dodavanje.value.some(p2 => p2.idPredmeta == p1.idPredmeta)
    )
    predmeti.forEach(element => {
        let elementZaSlanje = {
            "idStudenta": props.data.values.idStudenta,
            "idPredmeta": element.idPredmeta,
            "skolskaGodina": `${new Date().getFullYear()}/${(new Date().getFullYear() + 1).toString().slice(-2)}`
        }
        axios.post(`http://pabp.viser.edu.rs:8000/api/StudentPredmets`, elementZaSlanje)
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
                <Predmet v-for="p in predmeti" :data="p" @handlePredmet="(arg, req) => handlePredmet(arg, req)"
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