<script setup>
const props = defineProps(["data","toggle","zapisnikStudneta"])
import PORT from '../assets/PORT'
</script>

<template>
    <tr :style="!props.zapisnikStudneta.includes(props.data.idPredmeta) || props.toggle ? {} : {backgroundColor:'yellow'}" class="tr-hover">
        <td>{{ props.data.idPredmeta }}</td>
        <td>{{ props.data.idProfesora }}</td>
        <td>{{ props.data.naziv }}</td>
        <td>{{ props.data.espb }}</td>
        <td>{{ props.data.status }}</td>
        <td><button @click="$emit('handlePredmet', data, 'brisanjePredmeta')" class="brisanje">Obrisi</button></td>
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
