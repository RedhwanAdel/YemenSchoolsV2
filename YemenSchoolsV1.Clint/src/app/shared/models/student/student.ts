// src/app/interfaces/student.ts
export interface StudentById {
    id: string;
    registerNo: string;
    nameAr: string;
    nameEn: string;
    nationality: string;
    address: string;
    gender: number; // ممكن enum (مثلاً 0 = Female, 1 = Male)
    dateOfBirth: string; // أو Date لو هتتعامل مع تاريخ
    phoneNumber: string;
    email: string;
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
    Name: string;
    className: string | null;
    sectionName: string | null;
}
export interface StudentList {
    id: string;
    name: string;
    registerNo: string;
}
export interface CreateStudentDto {
    registerNo: string;
    nameAr: string;
    nameEn: string;
    phoneNumber: string;
    address: string;
    email: string;
    nationality: string;
    dateOfBirth: string; // الأفضل أن تكون سلسلة نصية لتطابق تنسيق JSON
    gender: number;     // 1 للذكر، 2 للأنثى مثلاً
    schoolId: string;
    currentAcademicYearId: string;
    currentSectionId: string;
    parents: ParentAssociationDto[];
}
export interface ParentAssociationDto {
    parentId: string;
    relationType: string; // مثال: "أب", "أم", "ولي أمر"
}
export interface StudentStatus {
    [studentId: string]: number; // 0: Present, 1: AbsentWithoutExcuse, etc.
}
export interface CreateAttendanceRequest {
    sectionId: string;
    date: string;
    studentStatuses: StudentStatus;
}

export interface UpdateAttendanceRequest {
    studentStatuses: StudentStatus;
}
