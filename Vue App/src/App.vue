<script setup>
import { reactive, ref, onMounted } from 'vue';
import IzmenaPodataka from './components/IzmenaPodataka.vue';
import Studenti from './components/Studenti.vue';
import axios from 'axios';
import Predmeti from './components/Predmeti.vue';

defineEmits(["handleRequest"])

let inicijalniStudenti = ref([])
let podaciZapisnika = ref([])

const studentZaIzmenu = reactive({})
const prikazPredmeta = reactive({})
const showIzmeni = ref(false)

const dohvatiPodatke = async () => {
    
    let dohvaceniStudenti = await axios.get("http://pabp.viser.edu.rs:8000/api/Students")
    let dohvaceniZapisnik = await axios.get("http://pabp.viser.edu.rs:8000/api/Zapisniks")
    inicijalniStudenti.value = await dohvaceniStudenti.data
    podaciZapisnika.value = await dohvaceniZapisnik.data
    showIzmeni.value = false
}

const handleRequest = (vrednost, req) => {
    if (req == 'izmena') {
        showIzmeni.value = true
        studentZaIzmenu.values = vrednost
    } else if (req == 'predmeti') {
        showIzmeni.value = false
        prikazPredmeta.values = vrednost
    }
}


onMounted(() => dohvatiPodatke())
</script>

<template>
    <div class="diplayFlex">
        <div class="studenti">
            <Studenti v-if="!prikazPredmeta.values" :inicijalniStudenti="inicijalniStudenti" :podaciZapisnika="podaciZapisnika"
                @handleRequest='(arg, req) => handleRequest(arg, req)' />
        </div>
        <div>
            <IzmenaPodataka v-show="showIzmeni" :data=studentZaIzmenu @izmenaRefresh="() => dohvatiPodatke()" />
            <Predmeti :data=prikazPredmeta></Predmeti>
        </div>
    </div>
</template>

<style scoped>
.diplayFlex {
    display: flex;
}

.studenti {
    width: 30vw;
}
</style>
