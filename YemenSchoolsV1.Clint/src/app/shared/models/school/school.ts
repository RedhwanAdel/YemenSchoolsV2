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
