import { SubjectReportDto } from "../mark/mark";

export interface StudentInfo {
    studentId: string;
    name: string;
    school: string;
    grade: string;
    section: string;
}

export interface StudentReport {
    student: StudentInfo;
    subjects: SubjectReportDto[];
    final?: {
        total: number;
        maxTotal: number;
        percentage: string;
        grade: string;
    };
}
