export interface YearDto {
    id: string;
    name: string;
    startDate: Date;
    endDate: Date;
    isCurrentYear: boolean
    isCurrentYearDisplay?: string;
}

export interface CreateYearDto {
    id: string;
    name: string;
    schoolId: string;
    startDate: Date;
    endDate: Date;
}
