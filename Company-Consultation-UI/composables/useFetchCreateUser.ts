// useCreateUser.ts
import { ref, computed } from 'vue';
import type { CreateUser, CreateResponse } from '../interfaces/AuthFetch';
import { createUser } from '../services/auth-Service';

export const useCreateUser = () => {
  const createUserResponse = ref<CreateResponse | null>(null);
  const createUserError = ref<string | null>(null);
  const createUserLoading = ref<boolean>(false);

  const createUserFunc = async (userData: CreateUser) => {
    createUserLoading.value = true;
    createUserResponse.value = null;
    createUserError.value = null;
    try {
      const response = await createUser(userData);
      if (response.status && response.status >= 400) {
        createUserError.value = response.data.message || 'Erro ao criar usuário';
      } else {
        createUserResponse.value = response;
      }
    } catch (error: any) {
      createUserError.value = error.response?.data?.message || error.message || 'Erro ao criar usuário';
    } finally {
      createUserLoading.value = false;
    }
  };

  return {
    createUserResponse: computed(() => createUserResponse.value),
    createUserError: computed(() => createUserError.value),
    createUserLoading: computed(() => createUserLoading.value),
    createUserFunc,
  };
}
