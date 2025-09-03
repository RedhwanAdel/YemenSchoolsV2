export class SchoolParams {
    pageNumber: number = 1;
    pageSize: number = 10;
    orderBy: SchoolOrdering = SchoolOrdering.Rating;
    search?: string;
    cityId?: string;
    regionId?: string;
    sortDirection: 'asc' | 'desc' = 'desc';
    type?: number;     // SchoolType (enum as number)
    gender?: number;   // GenderType (enum as number)
    levels: number = 0; // SchoolLevel as flags
}
export enum SchoolOrdering {
    Rating = 0,
    Name = 1,
    City = 2,
    Region = 3
}
