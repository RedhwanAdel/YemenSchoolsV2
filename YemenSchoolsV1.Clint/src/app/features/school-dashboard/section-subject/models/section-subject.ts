export interface SectionSubject {
    id: string;
    sectionId: string;
    subjectId: string;
    termId: string;
    teacherId: string;
    teacherName?: string;
    subjectName?: string;
    termName?: string;
}

export interface CreateSectionSubjectDto {
    sectionId: string;
    gradeSubjectId: string;
    termId: string;
    teacherId: string;
}

export interface SectionSubjectUpdateDto {
    id: string;
    gradeSubjectId: string;
    termId: string;
    teacherId: string;
    sectionId: string;
}
