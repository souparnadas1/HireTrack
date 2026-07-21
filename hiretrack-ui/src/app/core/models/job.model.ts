export interface Job {
  id: number;
  title: string;
  description: string;
  location: string;
  jobType: string;
  salary: number | null;
  deadline: string;
  isActive: boolean;
  employerName: string;
  categoryName: string;
}

export interface CreateJobRequest {
  title: string;
  description: string;
  location: string;
  jobType: string;
  salary: number | null;
  deadline: string;
  categoryId: number;
}
