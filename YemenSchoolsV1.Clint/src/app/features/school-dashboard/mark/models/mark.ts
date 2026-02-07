// يمثل كائن الدرجة التي سترسلها لل API
export interface CreateMarksDto {
    sectionSubjectId: string;
    assessmentType: string;
    maxScore: number;
    studentScores: { [key: string]: number };
}

// يمثل الطالب
export interface Student {
    id: string;
    firstName: string;
    lastName: string;
}

// يمثل العلاقة بين الشعبة والمادة والمعلم
export interface SectionSubject {
    id: string;
    sectionName: string;
    subjectName: string;
    sectionId: string;
    gradeName: string;
}

// يمثل نوع التقييم
export interface AssessmentType {
    value: string;
    viewValue: string;
}
export interface SubjectReportDto {

    studentId?: string;
    name: string;             // اسم المادة
    score: number;            // مجموع الدرجات
    grade: string;            // التقدير (ممتاز/جيد ...إلخ)
    details: SubjectDetails;  // تفاصيل المادة (درجات + حضور)
}

export interface SubjectDetails {
    grades: GradeDetail[];    // تفاصيل كل اختبار/واجب
}

export interface GradeDetail {
    type: string;             // نوع التقييم (اختبار أول، نهائي...)
    score: number;            // الدرجة التي حصل عليها
    total: number;            // الدرجة الكاملة
    percentage: string;       // النسبة (مثلاً 95%)
}

