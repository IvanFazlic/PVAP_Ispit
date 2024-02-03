<script setup>
import { computed } from 'vue'
import PORT from '../assets/PORT'
const props = defineProps(["data", "podaciZapisnika"])


const prosek = computed(()=>{
  let suma = 0
  let brojPolozenihIspita = 0
  if(props.podaciZapisnika.length > 0){
    props.podaciZapisnika.forEach(element => {
      if(element.idStudenta == props.data.idStudenta && Number(element.ocena) > 5){
        suma += element.ocena
        brojPolozenihIspita += 1
      }
    });
  }
  return brojPolozenihIspita ? Number(suma/brojPolozenihIspita).toFixed(2) :  "/"
})

</script>

<template>
  <tr class="tr-hover">
    <td>{{ data.ime }}</td>
    <td>{{ data.prezime }}</td>
    <td>{{ data.indeks}}</td>
    <td>{{ prosek }}</td>
    <td><button @click="$emit('handleRequest', data, 'izmena')" class="izmena">Izmena</button></td>
    <td><button @click="$emit('handleRequest', data, 'predmeti')" class="prikaziPredmete">Prikazi predmete</button></td>
  </tr>
</template>

<style scoped>
table,
th,
td {
  border: 1px solid black;
  border-collapse: collapse;
}
</style>
