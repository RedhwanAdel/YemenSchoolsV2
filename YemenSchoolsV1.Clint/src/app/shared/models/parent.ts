
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
