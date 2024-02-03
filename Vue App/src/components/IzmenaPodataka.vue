<script setup>
import { watch, ref } from 'vue';
import axios from 'axios';
import PORT from '../assets/PORT'
const props = defineProps(["data"])
const emits = defineEmits(["izmenaRefresh"])

let studentZaIzmenu = ref({})
let identification = ref(-1)
let ime = ref("")
let prezime = ref("")

const CleanUp = ()=> {
    identification.value = -1
    ime.value = undefined
    prezime.value = undefined
}

watch(props, () => {
    studentZaIzmenu = props.data.values

    identification.value = studentZaIzmenu.idStudenta
    ime.value = studentZaIzmenu.ime
    prezime.value = studentZaIzmenu.prezime
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
    studentZaIzmenu.ime = ime.value.toLowerCase().charAt(0).toUpperCase() + ime.value.slice(1);
    studentZaIzmenu.prezime = prezime.value.toLowerCase().charAt(0).toUpperCase() + prezime.value.slice(1);
    if (studentZaIzmenu.id != -1) {
        axios.put(`https://localhost:${PORT}/api/Students/${identification.value}`, studentZaIzmenu)
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
        <input type="text" id="lname" name="lname" v-model="prezime"><br><br>
        <input v-if="identification != -1" type="submit" value="Izmeni" @click="izmeniStudenta()" class="izmena" style="width: 60px;border-radius: 3px;">
    </div>
</template>

<style scoped></style>
