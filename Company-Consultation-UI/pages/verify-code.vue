<template>
    <div>
        <v-card class="mx-auto pa-12 pb-8 my-6" elevation="8" max-width="448" rounded="lg">
            <div class="text-center">
                <h1 class="mb-4">Verificar Código</h1>
                <v-divider></v-divider>
            </div>

            <div class="text-subtitle-1 text-medium-emphasis mt-4">Código de Verificação</div>

            <v-text-field v-model="code" density="compact" placeholder="Código de Verificação"
                prepend-inner-icon="mdi-email-outline" variant="outlined"></v-text-field>

            <v-btn class="mb-8" color="blue" size="large" variant="tonal" block @click="verifyCode">
                Verificar
            </v-btn>
        </v-card>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useVerifyCode } from '../composables/useVerifyCode.ts';
import { toast } from 'vue3-toastify';

const { verifyCodeFunc, verifyCodeError } = useVerifyCode();
const router = useRouter();
const code = ref('');
const email = ref('');

onMounted(() => {
    toast.info('Verifique seu email para obter o código de verificação');
});

const verifyCode = async () => {
    if (code.value === '') {
        toast.error('Por favor, insira o código de verificação');
        return;
    }

    email.value = localStorage.getItem('email') || '';
    localStorage.removeItem('email');

    await verifyCodeFunc({ code: code.value, email: email.value });
    if (verifyCodeError.value) {
        code.value = '';
        toast.error(verifyCodeError.value);
        return;
    }

    toast.success('Código verificado com sucesso');
    setTimeout(() => {
        router.push('/auth');
    }, 4000);
}
</script>

<style scoped></style>