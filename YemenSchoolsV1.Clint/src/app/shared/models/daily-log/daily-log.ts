export interface CreateDailyLogDto {
    lessonCovered: string;       // الدرس الذي تم شرحه
    homeworkAssigned: string;    // الواجب المنزلي
    teacherNotes?: string;       // ملاحظات المعلم (اختياري)
    sectionSubjectId: string;    // معرف الشعبة + المادة
}
export interface DailyLogDto {
    id: string;
    lessonCovered: string;
    homeworkAssigned: string;
    teacherNotes?: string;
    date: string;
    subjectName: string; // أضفنا اسم المادة

    sectionSubjectId: string;
    teacherId: string;
}

export interface DailyLog {
    id: string;
    lessonCovered: string;
    homeworkAssigned: string;
    teacherNotes?: string;
    date: string; // يمكن تحويلها إلى Date في TS
    sectionSubjectId: string;
}
