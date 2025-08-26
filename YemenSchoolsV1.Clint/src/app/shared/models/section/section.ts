export interface Section {
    id: string;
    name: string;
    academicYearId?: string;
    schoolGradeId?: string;
    gradeName?: string;
    capacity: number;
}
export interface SectionsOfYear {
    sectionId: string;
    sectionName: string;
    gradeName: string;
    subjectCount: number;
}
export interface SectionSubjectInfoDto {
    sectionId: string;
    gradeSubjectId: string;
    termId: string;
    teacherId: string;
    SubjectId: string;

    subjectName: string;
    gradeName: string;
    termName: string;
    teacherName: string;
}
