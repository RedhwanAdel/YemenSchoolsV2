export interface SchoolModel {
    id: string;
    name: string;
    logo: string | null;
    schoolType: string;
    genderType: string;
    city: string;
    region: string;
}

export interface SchoolPaginationModel {
    PageNumber: number;
    PageSize: number;
    OrderBy: number;
    Search: string;
    CityId: string;
    RegionId: string;
}