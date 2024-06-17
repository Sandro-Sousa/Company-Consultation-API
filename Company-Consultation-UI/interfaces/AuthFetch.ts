export interface CreateUser {
    name: string;
    email: string;
    password: string;
}

export interface CreateResponse {
    id: number;
    name: string;
    email: string;
  }

export interface AuthenticatedUser {
    email: string;
    password: string;
}

export interface AuthResponse {
    token: string;
    id: string;
    name: string;
    email: string;
  }

  export interface VerifyCode {
    email: string;
    code: string;
  }

