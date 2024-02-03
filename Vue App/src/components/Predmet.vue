<script setup>
const props = defineProps(["data","toggle"])

</script>

<template>
    <tr :style="props.data.ocena > 5 || props.toggle ? {} : {backgroundColor:'yellow'}" class="tr-hover">
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
