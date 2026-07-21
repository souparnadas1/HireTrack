export interface Application {
  id: number;
  candidateName: string;
  candidateEmail: string;
  jobTitle: string;
  status: string;
  appliedAt: string;
}

export interface CreateApplicationRequest {
  jobId: number;
}

export interface UpdateStatusRequest {
  status: string;
}
