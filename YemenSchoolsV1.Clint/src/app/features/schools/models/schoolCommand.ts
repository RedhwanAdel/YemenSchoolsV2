
export interface CreateSchoolDto {
    nameAr: string;
    nameEn: string;
    addressAr: string;
    addressEn: string;
    postalCode: string;
    mainPhone: string;
    email: string;
    schoolType: number;      // يمكن لاحقًا استبداله بـ enum
    genderType: number;      // كذلك
    curriculumType: number;
    schoolLevel: number;
    cityId: string;
    regionId: string;
}