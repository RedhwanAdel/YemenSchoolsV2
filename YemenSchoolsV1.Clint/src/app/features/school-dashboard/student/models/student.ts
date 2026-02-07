// src/app/features/school-dashboard/student/models/student.ts
export interface StudentById {
    id: string;
    registerNo: string;
    nameAr: string;
    nameEn: string;
    nationality: string;
    address: string;
    gender: number;
    dateOfBirth: string;
    phoneNumber: string;
    email: string;
}

export interface StudentProfileDto extends StudentById {
    // Add other profile fields returned by GetStudentProfileWithParentsQuery
    parents: ParentAssociationDto[];
    schoolId: string;
    currentAcademicYearId: string;
    currentSectionId: string;
    className?: string;
    sectionName?: string;
}

export interface Student {
    id: number;
    name: string;
    school: string;
    grade: string;
    avg: number;
    last: number;
    avatar: string;
    attendance: {
        present: number;
        absent: number;
        late: number;
    };
    timetable: string[][];
    grades: {
        subject: string;
        exam: number;
        homework: number;
        final: number;
    }[];
}

export interface StudentListDto {
    id: string;
    name: string; // Adjusted to match lowercase commonly used or mapping
    className: string | null;
    sectionName: string | null;
}

export interface CreateStudentDto {
    registerNo: string;
    nameAr: string;
    nameEn: string;
    phoneNumber: string;
    address: string;
    email: string;
    nationality: string;
    dateOfBirth: string;
    gender: number;
    schoolId: string;
    currentAcademicYearId: string;
    currentSectionId: string;
    parents: ParentAssociationDto[];
}

export interface UpdateStudentProfileDto {
    id: string;
    registerNo: string;
    nameAr: string;
    nameEn: string;
    nationality: string;
    address: string;
    gender: number;
    dateOfBirth: string;
    phoneNumber: string;
    email: string;
}

export interface ParentAssociationDto {
    parentId: string;
    relationType: string;
}

export interface StudentStatus {
    [studentId: string]: number;
}

export interface CreateAttendanceRequest {
    sectionId: string;
    date: string;
    studentStatuses: StudentStatus;
}

export interface UpdateAttendanceRequest {
    studentStatuses: StudentStatus;
}

export interface PromoteStudentsDto {
    studentIds: string[];
    newSectionId: string;
}

export class StudentParams {
    search?: string;
    pageNumber: number = 1;
    pageSize: number = 10;
    className?: string;
    sectionName?: string;
    academicYear?: string;
}
