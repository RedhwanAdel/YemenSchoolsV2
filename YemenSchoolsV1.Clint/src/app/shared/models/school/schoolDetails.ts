export interface SchoolDetails {
    id: string;
    name: string;
    description: string | null;
    address: string;
    logo: string;
    coverImage: string;
    postalCode: string;
    mainPhone: string;
    email: string;
    schoolType: 'Private' | 'Public' | string;
    isActive: boolean;
    genderType: 'Boys' | 'Girls' | 'Mixed' | string;
    curriculumType: 'National' | 'International' | string;
    schoolLevel: 'Kindergarten' | 'Primary' | 'Secondary' | string;
    cityName: string;
    regionName: string;
    phoneNumberList: string[];
}
