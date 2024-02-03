<script setup>
import { watch, ref } from 'vue';
import axios from 'axios';
const props = defineProps(["data"])
const emits = defineEmits(["izmenaRefresh"])
const SMEROVI = ["net","rt","nrt","is","rin","elin","elite","asuv"]

let studentZaIzmenu = ref({})
let identification = ref(-1)
let ime = ref("")
let prezime = ref("")
let smer = ref("")
let broj = ref("")
let godina = ref("")

const CleanUp = ()=> {
    identification.value = -1
    ime.value = undefined
    prezime.value = undefined
    smer.value = undefined
    broj.value = undefined
    godina.value = undefined
}

watch(props, () => {
    studentZaIzmenu = props.data.values

    identification.value = studentZaIzmenu.idStudenta
    ime.value = studentZaIzmenu.ime
    prezime.value = studentZaIzmenu.prezime
    smer.value = studentZaIzmenu.smer
    broj.value = studentZaIzmenu.broj
    godina.value = studentZaIzmenu.godinaUpisa
})
function izmeniStudenta() {
    let validacijaImena = /^[a-z ,.'-]+$/i
    let validacijaPrezimena = /^[a-z ,.'-]+$/i
    if(!validacijaImena.test(ime.value)){
        alert("Pogresan format imena")
        return
    }
    if(!validacijaPrezimena.test(prezime.value)){
        alert("Pogresan format prezimena")
        return
    }
    if(!SMEROVI.includes(smer.value.toLowerCase())){
        alert("Pogresan smer unet.")
        return
    }
    if(broj.value <= 0 || broj.value > 1500){
        alert("Pogresan broj indeksa unet.")
        return
    }
    if(godina.value <= 1990 || godina.value > new Date().getFullYear()){
        alert("Pogresana godina indeksa uneta.")
        return
    }
    studentZaIzmenu.ime = ime.value.toLowerCase().charAt(0).toUpperCase() + ime.value.slice(1);
    studentZaIzmenu.prezime = prezime.value.toLowerCase().charAt(0).toUpperCase() + prezime.value.slice(1);
    studentZaIzmenu.smer = smer.value.toUpperCase()
    studentZaIzmenu.broj = Number(broj.value)
    studentZaIzmenu.godinaUpisa = godina.value.toString()
    if (studentZaIzmenu.id != -1) {
        axios.put(`http://pabp.viser.edu.rs:8000/api/Students/${identification.value}`, studentZaIzmenu)
        .then(() => {
            emits('izmenaRefresh')
        }).catch((err) => {
            alert(err)
        }).finally(() => {
            CleanUp()
            alert("Uspesno izmenjen student.")
        })
    }
    CleanUp()
}
</script>

<template>
    <div>
        <h2>Forma sa izmenu studenta</h2>
        <label for="fname">Ime:</label><br>
        <input type="text" id="fname" name="fname" v-model="ime"><br>
        <label for="lname">Prezime:</label><br>
        <input type="text" id="lname" name="lname" v-model="prezime"><br>
        <label for="lsmer">Smer:</label><br>
        <input type="text" id="lsmer" name="lsmer" v-model="smer"><br>
        <label for="lbroj">Broj:</label><br>
        <input type="number" id="lbroj" name="lbroj" v-model="broj"><br>
        <label for="lgodina">Godina:</label><br>
        <input type="number" id="lgodina" name="lgodina" v-model="godina"><br><br>
        <input v-if="identification != -1" type="submit" value="Izmeni" @click="izmeniStudenta()" class="izmena" style="width: 60px;border-radius: 3px;">
    </div>
</template>

<style scoped></style>
