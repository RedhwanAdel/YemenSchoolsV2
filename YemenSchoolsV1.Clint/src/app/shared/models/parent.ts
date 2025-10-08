
export interface ParentCheckDto {
    id: string | null;
    nameAr: string | null;
    exists: boolean;

}
export interface StudentWithSchoolInfoDto {
    studentId: string;
    studentName: string;
    imageUrl: string | null;
    schoolName: string | null;
    className: string | null;
    sectionName: string | null;
    avg: number;
}

export interface TeacherInfoForParentDto {
    teacherId: string;       // Guid من الـ backend راح يجي كـ string
    userId: string;       // Guid من الـ backend راح يجي كـ string
    teacherName: string;
    teacherPhoto?: string;
    schoolName: string;
    gradeName: string;
    sectionName: string;
    subjectName: string;
    studentName: string;
}

