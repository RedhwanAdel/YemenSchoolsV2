export interface Subject {
    id: string;
    name: string;
    gradeSubjectId?: string;
}

export interface CreateSubjectDto {
    name: string;
    arName: string;
    enName: string;
    code: string;
    gradeId?: string; // If linked on creation
    schoolId?: string;
}

export interface UpdateSubjectDto {
    id: string;
    name: string;
    arName: string;
    enName: string;
    code: string;
}
