export interface RegisterRequest {
  fullname: string;
  email: string;
  password: string;
  role: string;
}

export interface loginRequest {
  email: string;
  password: string;
}

export interface TokenResponse {
  token: string;
  email: string;
  fullname: string;
  role: string;
  expiresAt: string;
}
