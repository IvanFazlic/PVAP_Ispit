<script setup>
import { ref, computed, watch } from 'vue';
import Student from './Student.vue';
defineEmits(["handleRequest"])
const props = defineProps(["inicijalniStudenti", "podaciZapisnika"])
const kriterijumPretrage = ref("")

const studenti = computed(() => {
    return props.inicijalniStudenti.filter(student => {
        for (let studentKey in student) {
            if (student[studentKey] != null && student[studentKey]
                .toString()
                .toLowerCase()
                .includes(String(kriterijumPretrage.value.toLowerCase()))) return 1
        }
    })
}
)
</script>

<template>
    Pretraga: <input type="text" v-model="kriterijumPretrage">
    <br><br>
    <table>
        <thead>
            <th>Ime</th>
            <th>Prezime</th>
            <th>Broj indeksa</th>
            <th>Prosek</th>
        </thead>
        <tbody>
            <Student v-for="student in studenti" :data="student" :podaciZapisnika="props.podaciZapisnika"
                @handleRequest="(arg, req) => $emit('handleRequest', arg, req)" />
        </tbody>
    </table>
</template>

<style scoped>
table,
th,
td {
    border: 1px solid black;
    border-collapse: collapse;
}
</style>
