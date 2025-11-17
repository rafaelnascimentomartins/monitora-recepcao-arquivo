export class NavigationRoutesUtil {
    static navigateToRoute(route: string): void {
        window.location.href = route;
    }
}