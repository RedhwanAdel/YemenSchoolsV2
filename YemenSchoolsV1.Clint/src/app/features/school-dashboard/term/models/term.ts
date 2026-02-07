export interface Term {
    id: string;
    name: string;
    academicYearName: string;
    startDate: Date;
    endtDate: Date;
}

export interface TermDto extends Term { }

export interface CreateTermDto {
    name: string;
    startDate: Date;
    endDate: Date;
    academicYearId: string;
}

export interface UpdateTermDto extends CreateTermDto {
    id: string;
}
