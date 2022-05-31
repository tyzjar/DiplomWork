function  IM_OUT = gui_orientation(sim, ims)

[M, N, K] = size(sim);

MAX = max([M, N, K]);

[X, Y, Z] = ind2sub([M, N, K], 1 : M * N * K);

%center of masses

X0 = X * sim(:);
Y0 = Y * sim(:);
Z0 = Z * sim(:);

%principial axes

Cxx = (X - X0) .* (X - X0) * sim(:);
Cxy = (X - X0) .* (Y - Y0) * sim(:);
Cxz = (X - X0) .* (Z - Z0) * sim(:);
Cyy = (Y - Y0) .* (Y - Y0) * sim(:);
Cyz = (Y - Y0) .* (Z - Z0) * sim(:);
Czz = (Z - Z0) .* (Z - Z0) * sim(:);

C = [ ...
    Cxx     Cxy     Cxz
    Cxy     Cyy     Cyz
    Cxz     Cyz     Czz];

[V, D] = eig(C);

if ims == 1
    Dref = D;
else
    V = V * (D / Dref) .^ 0.5;
end

Dmem(ims, :) = [D(1, 1), D(2, 2), D(3, 3)];

V = [V(2, :); V(1, :); V(3, :)];
V = [V(:, 3), V(:, 2), V(:, 1)];

%applying transform

shift = [[eye(3), ...
    2 / MAX * [- 0.5 * N + Y0; - 0.5 * M + X0; - 0.5 * K + Z0]]; ...
    [zeros(1, 3), 1]];
rot = [[V, zeros(3, 1)]; [zeros(1, 3), 1]];
tform = mcombt(shift, rot);

%choosing proper orientation

if strcmp(imref, 'noref')
    im = mtform(im, tform);

else
    tformopt = zeros(4, 4, 4);

    tformopt(1, :, :) = tform;
    tformopt(2, :, :) = mcombt(tform, diag([-1, -1, 1, 1]));
    tformopt(3, :, :) = mcombt(tform, diag([1, -1, -1, 1]));
    tformopt(4, :, :) = mcombt(tform, diag([-1, 1, -1, 1]));

    c = zeros(4, 1);

    for i = 1 : 4
        c(i) = mcov(mtform(double(sim > 0), squeeze(tformopt(i, :, :))), ...
            double(simref > 0));
    end

    [~, b] = max(c);

    tform = squeeze(tformopt(b, :, :));
    im = mtform(im, tform);
end

%determining non-empty volume

if ims == 1
    reg1 = double(max(max(im, [], 3), [], 2) > 0.05 * max(im(:)));
    reg2 = double(max(max(im, [], 3), [], 1) > 0.05 * max(im(:)));
    reg3 = double(max(max(im, [], 2), [], 1) > 0.05 * max(im(:)));
else
    reg1 = reg1 + double(max(max(im, [], 3), [], 2) > 0.05 * max(im(:)));
    reg2 = reg2 + double(max(max(im, [], 3), [], 1) > 0.05 * max(im(:)));
    reg3 = reg3 + double(max(max(im, [], 2), [], 1) > 0.05 * max(im(:)));
end

if ims ~= 1
    nzind = find(im > 0);
    imh = cell2mat(varargout(1));
    im(nzind) = mequalize(im(nzind), imh(imh > 0));
end

%showing 3D projections

pim = [max(im, [], 3), squeeze(max(im, [], 2)); ...
    squeeze(max(im, [], 1))', zeros(size(im, 3))];

figure

imagesc(pim); axis image; colormap hot; drawnow

IM_OUT = im;
