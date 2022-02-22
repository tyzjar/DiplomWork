function gui_originalView(sample, koef)
    imOriginal=Rload(sample,1,koef);
    figure
    image(vol2proj(imOriginal),'CDataMapping','scaled');
    axis image
    colormap(jet) %parula
    colorbar();
    title('Original');
end