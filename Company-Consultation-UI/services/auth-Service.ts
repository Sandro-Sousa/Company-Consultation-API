import axios from 'axios';
import type {CreateUser, CreateResponse, AuthenticatedUser, VerifyCode, AuthResponse} from '../interfaces/AuthFetch';


const BASE_URL = 'https://localhost:7279/api/Login/v1/';

export const authenticateUser = async (authenticatedUser: AuthenticatedUser): Promise<AuthResponse | any> => {
    try {
        const response = await axios.post(`${BASE_URL}authenticate`, authenticatedUser);
        return response.data;   
    } catch (error: any) {
        if (error.response) {
            return {
                status: error.response.status,
                data: error.response.data
            };
        } else if (error.request) {
            return { message: 'No response received' };
        } else {
            return { message: error.message };
        }
    }
};

export const createUser = async (createUser: CreateUser): Promise<CreateResponse | any> => {
    try {
        const response = await axios.post(`${BASE_URL}createUser`, createUser);
        return response.data;
    } catch (error: any) {
        if (error.response) {
            return {
                status: error.response.status,
                data: error.response.data
            };
        } else if (error.request) {
            return { message: 'No response received' };
        } else {
            return { message: error.message };
        }
    }
};

export const verifyCode = async (verifyCode: VerifyCode): Promise<AuthResponse | any> => {
    try {
        const response = await axios.post(`${BASE_URL}verifyCode`, verifyCode);
        return response.data;
    } catch (error: any) {
        if (error.response) {
            return {
                status: error.response.status,
                data: error.response.data
            };
        } else if (error.request) {
            return { message: 'Erro!!!' };
        } else {
            return { message: error.message };
        }
    }
};

