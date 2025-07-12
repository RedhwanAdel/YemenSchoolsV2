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
