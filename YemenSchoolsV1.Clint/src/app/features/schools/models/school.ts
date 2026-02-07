export type { Subject } from "@features/school-dashboard/school-subject/models/subject";

export interface SchoolListItem {
    id: string;
    name: string;
    logo: string | null;
    coverImage: string | null;
    mainPhone: string;
    schoolType: 'Private' | 'Public';
    genderType: 'Boys' | 'Girls' | 'Mixed';
    city: string;
    region: string;
    schoolLevel: string;
    curriculumType: string;
    averageRating: number;


}
export interface SchoolForUpdate {
    id: string;
    nameAr: string;
    nameEn: string;
    addressAr: string;
    addressEn: string;
    postalCode: string;
    mainPhone: string;
    email: string;
    schoolType: number;
    genderType: number;
    curriculumType: number;
    schoolLevel: number;
    cityId: string;
    cityName: string;
    regionId: string;
    regionName: string;
    phoneNumberList: string[];
}
export interface CreateSchoolGradeDto {
    schoolId: string;
    stageGradeIds: string[];
}
export interface StageGradeDto {
    stageGradeId: string;
    stageName?: string;
    gradeName?: string;
    isSelected: boolean;
}
export interface SchoolGradeSubject {
    stageGradeId: string;
    subjectIds: string[];
}
export interface SchoolGradeWithDetailsDto {
    id: string; // هذا هو SchoolGradeId من الباك إند
    stageName?: string;
    gradeName?: string;
    // أضف أي خصائص أخرى إذا كانت موجودة في الـ DTO
}

export interface AssignSubjectsToSchoolGradeDto {
    schoolGradeId: string; // استخدم هذا بدلاً من stageGradeId
    subjectIds: string[];
}
export interface SchoolGradeSubjectsInit {
    stageGradeId: string;
    subjectsIds: string[];
}
export interface SchoolReportData {
    schoolId: string;
    nameAr: string;
    nameEn: string;
    descriptionAr: string | null;
    addressAr: string;
    postalCode: string;
    mainPhone: string;
    email: string;
    schoolType: number; // قد تحتاج إلى تحويلها إلى اسم مفهوم لاحقًا (مثال: 'حكومي', 'خاص')
    schoolLevel: number; // قد تحتاج إلى تحويلها إلى اسم مفهوم
    genderType: number; // قد تحتاج إلى تحويلها إلى اسم مفهوم
    curriculumType: number; // قد تحتاج إلى تحويلها إلى اسم مفهوم
    cityId: string;
    cityNameAr: string;
    regionId: string;
    regionNameAr: string;
    phoneNumbers: any[]; // نوع البيانات حسب ما يأتي من الـ API
    teachersCount: number;
    studentsCount: number;
    gradesCount: number;
    subjectsCount: number;
    sectionsCount: number;
    academicYearsCount: number;
    newsCount: number;
    photosCount: number;
    parentsCount: number;
    ratingsCount: number;
}
export interface SchoolReview {
    id: string;
    schoolId: string;
    userId: string;
    userName: string;
    userImage: string;
    rating: number;
    comment?: string;
    createdAt: string;
}export interface SchoolPhoto {
    id: string;           // GUID
    schoolId: string;     // GUID للمدرسة
    photoUrl: string;     // رابط الصورة

}
