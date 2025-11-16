export class BaseFilter {
    page: number = 1;
    pageSize: number = 10;

    sortField: string|null = null;
    sortDirection: string = 'ASC';
}