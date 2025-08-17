// يمثل كائن الدرجة التي سترسلها لل API
export interface CreateMarksDto {
    sectionSubjectId: string;
    assessmentType: string;
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
}

// يمثل نوع التقييم
export interface AssessmentType {
    value: string;
    viewValue: string;
}