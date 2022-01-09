import { Outlet } from "react-router-dom";

export const PageLayout = () =>
    <div className="min-h-screen text-center bg-trueGray-900">
        <Outlet />
    </div>;
