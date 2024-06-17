import { ref, computed } from 'vue';
import type { Ref } from 'vue';
import { getCompany, consultCompanyService } from '../services/fetch-Company-Service';
import type { EmpresaParsed, Cnpj } from '../interfaces/CompanyData';

export const useFetchCompany = () => {
    const companyResponse: Ref<EmpresaParsed[] | null> = ref(null);
    const companyError: Ref<string | null> = ref(null);
    const companyLoading: Ref<boolean> = ref(false);

    const getAllCompany = async (): Promise<EmpresaParsed[] | null> => {
        try {
            companyLoading.value = true;
            companyResponse.value = null;
            companyError.value = null;

            const response = await getCompany();

            if (Array.isArray(response)) {
                companyResponse.value = response;
                return response;
            } else {
                throw new Error(response.message || 'Erro ao buscar empresas');
            }
        } catch (error: any) {
            console.error('Erro ao buscar empresas:', error);
            companyError.value = error.message || 'Erro ao buscar empresas';
            return null;
        } finally {
            companyLoading.value = false;
        }
    };

    const consultCompany = async (cnpj: Cnpj): Promise<void> => {
        companyLoading.value = true;
        companyResponse.value = null;
        companyError.value = null;

        try {
            const response = await consultCompanyService(cnpj);

            if (Array.isArray(response)) {
                companyResponse.value = response;
            } else {
                throw new Error(response.message || 'Erro ao consultar empresa');
            }
        } catch (error: any) {
            console.error('Erro ao consultar empresa:', error);
            companyError.value = error.message || 'Erro ao consultar empresa';
        } finally {
            companyLoading.value = false;
        }
    };

    return {
        companyResponse: computed(() => companyResponse.value),
        companyError: computed(() => companyError.value),
        companyLoading: computed(() => companyLoading.value),
        getAllCompany,
        consultCompany,
    };
};
