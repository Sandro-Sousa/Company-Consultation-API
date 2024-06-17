<template>
    <v-container>
        <v-row class="justify-end">
            <v-col cols="12">
                <v-card>
                    <v-card>
                        <v-card-text class="d-flex">
                            <v-card-text class="d-flex justify-start align-center">
                                <v-icon large class="mr-3">mdi-domain</v-icon>
                                Empresas
                            </v-card-text>
                            <v-card-text class="d-flex justify-end align-center">
                                <v-text-field label="Consultar Empresa" v-model="cnpj" append-icon="mdi-magnify" solo
                                    @click:append="searchCompany"></v-text-field>
                            </v-card-text>
                        </v-card-text>
                    </v-card>
                    <v-divider></v-divider>
                </v-card>
            </v-col>
        </v-row>
    </v-container>
    <v-container>
        <v-row>
            <v-col cols="12">
                <v-table theme="dark">
                    <thead>
                        <tr>
                            <th class="text-left">ID</th>
                            <th class="text-left">Name</th>
                            <th class="text-left">CNPJ</th>
                            <th class="text-left">Situação</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in items" :key="item.id">
                            <td>{{ item.id }}</td>
                            <td>{{ item.result.nome }}</td>
                            <td>{{ item.cnpj }}</td>
                            <td>{{ item.result.situacao }}</td>
                        </tr>
                    </tbody>
                </v-table>
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useFetchCompany } from '../composables/useFetchCompany';
import type { EmpresaParsed } from '../interfaces/CompanyData.ts';

const { getAllCompany, consultCompany, companyResponse } = useFetchCompany();

const items = ref<EmpresaParsed[]>([]);
const cnpj = ref('');

const fetchData = async () => {
    const response = await getAllCompany();
    if (response) {
        items.value = response;
    }
};

const searchCompany = async () => {
    if (cnpj.value) {
        await consultCompany({ cnpj: cnpj.value });
        cnpj.value = '';
        await fetchData();
    }
};

fetchData();
</script>

<style scoped></style>
