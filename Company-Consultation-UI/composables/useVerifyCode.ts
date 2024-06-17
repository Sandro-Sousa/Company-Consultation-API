import { ref, computed } from 'vue';
import type { VerifyCode, AuthResponse } from '../interfaces/AuthFetch';
import { verifyCode } from '../services/auth-Service';

export const useVerifyCode = () => {
  const verifyCodeResponse = ref<AuthResponse | null>(null);
  const verifyCodeError = ref<string | null>(null);
  const verifyCodeLoading = ref<boolean>(false);

  const verifyCodeFunc = async (userData: VerifyCode) => {
    verifyCodeLoading.value = true;
    verifyCodeResponse.value = null;
    verifyCodeError.value = null;
    try {
      verifyCodeResponse.value = await verifyCode(userData);
    } catch (error: any) {
      verifyCodeError.value = error.response.data.message || 'Erro ao verificar código';
    } finally {
      verifyCodeLoading.value = false;
    }
  };

  return {
    verifyCodeResponse: computed(() => verifyCodeResponse.value),
    verifyCodeError: computed(() => verifyCodeError.value),
    verifyCodeLoading: computed(() => verifyCodeLoading.value),
    verifyCodeFunc,
  };
};
