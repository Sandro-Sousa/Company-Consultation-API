<template>
  <div>
    <v-card class="mx-auto pa-12 pb-8 my-6" elevation="8" max-width="448" rounded="lg">
      <div class="text-center">
        <h1 class="mb-4">Login</h1>
        <v-divider></v-divider>
      </div>

      <v-text-field v-model="email" density="compact" placeholder="Email address" prepend-inner-icon="mdi-email-outline"
        variant="outlined"></v-text-field>

      <v-text-field v-model="password" :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
        :type="visible ? 'text' : 'password'" density="compact" placeholder="Enter your password"
        prepend-inner-icon="mdi-lock-outline" variant="outlined"
        @click:append-inner="visible = !visible"></v-text-field>

      <v-btn class="mb-8" color="blue" size="large" variant="tonal" block @click="login">
        Conecte-se
      </v-btn>

      <v-card-text class="text-center">
        <nuxt-link class="text-blue text-decoration-none" to="/register">
          Não tem uma conta? Inscrever-se
        </nuxt-link>
      </v-card-text>
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthenticateUser } from '../composables/useAuthenticateUser.ts';
import { toast } from 'vue3-toastify';

const email = ref('');
const password = ref('');
const router = useRouter();
const { authenticateUserFunc, authenticateUserResponse, authenticateUserError } = useAuthenticateUser();
const visible = ref(false);

const login = async () => {
  try {
    await authenticateUserFunc({ email: email.value, password: password.value });

    if (authenticateUserError.value) {
      email.value = '';
      password.value = '';
      toast.error(authenticateUserError.value);
      return;
    }

    if (authenticateUserResponse.value) {
      router.push('/companies');
    }
  } catch (error) {
    email.value = '';
    password.value = '';
    toast.error('Ocorreu um erro inesperado.');
    console.error(error);
  }
};
</script>

<style scoped></style>
