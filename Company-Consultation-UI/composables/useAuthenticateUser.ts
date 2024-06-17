import { ref, computed } from 'vue';
import type { AuthenticatedUser, AuthResponse } from '../interfaces/AuthFetch';
import { authenticateUser } from '../services/auth-Service';

export const useAuthenticateUser = () => {
  const authenticateUserResponse = ref<AuthResponse | null>(null);
  const authenticateUserError = ref<string | null>(null);
  const authenticateUserLoading = ref<boolean>(false);

  const authenticateUserFunc = async (userData: AuthenticatedUser) => {
    authenticateUserLoading.value = true;
    authenticateUserResponse.value = null;
    authenticateUserError.value = null;
    try {
      const response = await authenticateUser(userData);
      
      if (response.token) {
        authenticateUserResponse.value = response;
        sessionStorage.setItem('token', response.token);
      } else {
        authenticateUserError.value = response.data || 'Falha na autenticação';
      }
    } catch (error: any) {
      console.log(error, 'error');
      authenticateUserError.value = error.response?.data?.message || error.message || 'Erro ao fazer login';
    } finally {
      authenticateUserLoading.value = false;
    }
  };

  return {
    authenticateUserResponse: computed(() => authenticateUserResponse.value),
    authenticateUserError: computed(() => authenticateUserError.value),
    authenticateUserLoading: computed(() => authenticateUserLoading.value),
    authenticateUserFunc,
  };
};
