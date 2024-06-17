<template>
  <div>
    <v-card class="mx-auto pa-12 pb-8 my-6" elevation="8" max-width="448" rounded="lg">
      <div class="text-center">
        <h1 class="mb-4">Registrar</h1>
        <v-divider></v-divider>
      </div>

      <div class="text-subtitle-1 text-medium-emphasis">Nome</div>
      <v-text-field v-model="name" density="compact" placeholder="Nome" prepend-inner-icon="mdi-account-outline"
        variant="outlined"></v-text-field>

      <div class="text-subtitle-1 text-medium-emphasis">Email</div>
      <v-text-field v-model="email" density="compact" placeholder="Endereço de Email"
        prepend-inner-icon="mdi-email-outline" variant="outlined"></v-text-field>

      <div class="text-subtitle-1 text-medium-emphasis">Senha</div>
      <v-text-field v-model="password" :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
        :type="visible ? 'text' : 'password'" density="compact" placeholder="Digite sua senha"
        prepend-inner-icon="mdi-lock-outline" variant="outlined"
        @click:append-inner="visible = !visible"></v-text-field>

      <v-btn class="mb-8" color="blue" size="large" variant="tonal" block @click="register">
        Registrar
      </v-btn>
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useCreateUser } from '../composables/useFetchCreateUser.ts';
import { toast } from 'vue3-toastify';

const name = ref('');
const email = ref('');
const password = ref('');
const visible = ref(false);
const { createUserFunc, createUserError, createUserLoading } = useCreateUser();
const router = useRouter();

const register = async () => {
  await createUserFunc({ name: name.value, email: email.value, password: password.value });
  if (!createUserError.value) {
    localStorage.setItem('email', email.value);
    router.push('/verify-code');
  } else {
    name.value = '';
    email.value = '';
    password.value = '';
    toast.error('Ocorreu um Erro ao criar o usuário');
  }
};
</script>

<style scoped></style>