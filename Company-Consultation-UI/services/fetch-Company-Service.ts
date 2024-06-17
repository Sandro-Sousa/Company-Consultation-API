import axios from 'axios';
import type { EmpresaParsed, Empresa, Resultado, Cnpj } from '../interfaces/CompanyData';

const BASE_URL = 'https://localhost:7279/api/Order/v1/';

export const getCompany = async (): Promise<EmpresaParsed[] | any> => {
    try {
        const token = sessionStorage.getItem('token');
        if (!token) {
            throw new Error('Token não encontrado');
        }

        const response = await axios.get<Empresa[]>(`${BASE_URL}getAllOrders`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        const companiesWithParsedResults: EmpresaParsed[] = response.data.map((empresa) => {
            const parsedResult: Resultado = JSON.parse(empresa.result);
            return {
                ...empresa,
                result: parsedResult,
            };
        });

        return companiesWithParsedResults;
    } catch (error: any) {
        if (error.response && error.response.data) {
            return error.response.data;
        } 
    }
};

export const consultCompanyService = async (cnpj: Cnpj): Promise<any> => {
  try {
      const token = sessionStorage.getItem('token');
      if (!token) {
          throw new Error('Token não encontrado');
      }

      const response = await axios.post(`${BASE_URL}processOrder`, cnpj, {
          headers: {
              Authorization: `Bearer ${token}`,
          },
      });
      return response.data;
  } catch (error: any) {
      return error.response.data;
  }
};
