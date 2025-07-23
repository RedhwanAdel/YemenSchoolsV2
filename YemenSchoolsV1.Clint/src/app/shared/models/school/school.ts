import { Subject } from "./subject";

export interface SchoolListItem {
    id: string;
    name: string;
    logo: string | null;
    coverImage: string | null;
    mainPhone: string;
    schoolType: 'Private' | 'Public';
    genderType: 'Boys' | 'Girls' | 'Mixed';
    city: string;
    region: string;
    schoolLevel: string;
}
export interface SchoolForUpdate {
    id: string;
    nameAr: string;
    nameEn: string;
    addressAr: string;
    addressEn: string;
    postalCode: string;
    mainPhone: string;
    email: string;
    schoolType: number;
    genderType: number;
    curriculumType: number;
    schoolLevel: number;
    cityId: string;
    cityName: string;
    regionId: string;
    regionName: string;
    phoneNumberList: string[];
}
export interface CreateSchoolGradeDto {
    schoolId: string;
    stageGradeIds: string[];
}
export interface StageGradeDto {
    stageGradeId: string;
    stageName?: string;
    gradeName?: string;
    isSelected: boolean;
}
export interface SchoolGradeSubject {
    stageGradeId: string;
    subjectIds: string[];
}
export interface SchoolGradeWithDetailsDto {
    id: string; // هذا هو SchoolGradeId من الباك إند
    stageName?: string;
    gradeName?: string;
    // أضف أي خصائص أخرى إذا كانت موجودة في الـ DTO
}

export interface AssignSubjectsToSchoolGradeDto {
    schoolGradeId: string; // استخدم هذا بدلاً من stageGradeId
    subjectIds: string[];
}
export interface SchoolGradeSubjectsInit {
    stageGradeId: string;
    subjectsIds: string[];
}