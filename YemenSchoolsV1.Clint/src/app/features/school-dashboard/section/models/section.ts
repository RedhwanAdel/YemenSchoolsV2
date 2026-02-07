export interface Section {
    id: string;
    name: string;
    academicYearId?: string;
    schoolGradeId?: string;
    gradeName?: string;
    capacity: number;
    classTeacherId?: string; // Often returned in GetById
}

export interface CreateSectionDto {
    name: string;
    capacity: number;
    schoolGradeId: string;
    academicYearId: string;
    classTeacherId?: string;
}

export interface UpdateSectionDto {
    id: string;
    name: string;
    capacity: number;
    schoolGradeId: string;
    academicYearId: string;
    classTeacherId?: string;
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
