<script setup>
import {ref} from 'vue'
import PORT from '../assets/PORT'

const props = defineProps(["dataZapisnik"])
const zapisnik = ref(false)
</script>

<template>
    <tr v-if="props.dataZapisnik.ocena">
        <td>{{ props.dataZapisnik.idPredmeta }}</td>
        <td>{{ props.dataZapisnik.idProfesora }}</td>
        <td>{{ props.dataZapisnik.naziv }}</td>
        <td>{{ props.dataZapisnik.espb }}</td>
        <td>{{ props.dataZapisnik.status }}</td>
    </tr>
</template>

<style scoped>
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
button:hover{
    cursor: pointer;
}
</style>
