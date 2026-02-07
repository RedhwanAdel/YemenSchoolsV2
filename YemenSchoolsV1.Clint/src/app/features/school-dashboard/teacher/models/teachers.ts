export interface Teacher {
    id: string;
    name: string;
    email: string;
    phoneNumber: string;
    address: string;
    gender: number; // 1 = Male, 2 = Female
    hireDate: string; // أو Date إن كنت تحولها في الباك
    specialization: string;
    employmentStatus: string;
    profilePictureUrl: string;
    schoolId: string;
}

export interface CreateTeacherDto {
    name: string;
    email: string;
    phoneNumber: string;
    address: string;
    gender: number;
    hireDate: string;
    specialization: string;
    employmentStatus: string;
    profilePictureUrl: string;
    schoolId?: string;
}

export interface UpdateTeacherDto extends CreateTeacherDto {
    id: string;
}
