export type ApiResponse<T> = {
    statusCode: number;
    meta: any;
    succeeded: boolean;
    message: string;
    errors: any;
    errorsBag: any;
    data: T;
};
