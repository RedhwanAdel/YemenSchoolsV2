export interface YearDto {
    id: string;
    name: string;
    startDate: Date;
    endDate: Date;
}

export interface CreateYearDto {
    id: string;
    name: string;
    schoolId: string;
    startDate: Date;
    endDate: Date;
}
