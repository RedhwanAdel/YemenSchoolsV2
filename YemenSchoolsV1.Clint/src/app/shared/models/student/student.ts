// src/app/interfaces/student.ts
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