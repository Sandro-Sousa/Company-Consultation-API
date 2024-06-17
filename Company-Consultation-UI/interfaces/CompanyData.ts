export interface Atividade {
  code: string;
  text: string;
}

export interface Billing {
  free: boolean;
  database: boolean;
}

export interface Resultado {
  situacao: string;
  abertura: string;
  tipo: string;
  nome: string;
  fantasia: string;
  porte: string;
  natureza_juridica: string;
  atividade_principal: Atividade[];
  logradouro: string;
  numero: string;
  municipio: string;
  bairro: string;
  uf: string;
  cep: string;
  data_situacao: string;
  cnpj: string;
  ultima_atualizacao: string;
  status: string;
  complemento: string;
  email: string;
  telefone: string;
  efr: string;
  motivo_situacao: string;
  situacao_especial: string;
  data_situacao_especial: string;
  atividades_secundarias: Atividade[];
  capital_social: string;
  qsa: {
    nome: string;
    qual: string;
  }[];
  extra: Record<string, any>;
  billing: Billing;
}

export interface Empresa {
  cnpj: string;
  result: string;
  id: number;
}

export interface EmpresaParsed extends Omit<Empresa, 'result'> {
  result: Resultado;
}

export interface Cnpj {
  cnpj: string;
}
