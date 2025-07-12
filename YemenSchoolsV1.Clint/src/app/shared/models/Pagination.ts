export type Pagination<T> = {
    data: T[];
    pageSize: number;
    currentPage: number;
    totalPages: number;
    totalCount: number;
    meta: {
        count: number;
    };
    hasPreviousPage: boolean;
    hasNextPage: boolean;
    messages: string[];
    succeeded: boolean;
}