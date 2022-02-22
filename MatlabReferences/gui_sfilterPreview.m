function gui_sfilterPreview(sample,koef,Lowpass,Hipass)
    imOriginal=Rload(sample,1,koef);
    imFilter= sgauss(imOriginal, Lowpass,Hipass);
    figure
    image(vol2proj(imFilter),'CDataMapping','scaled');
    axis image
    colormap(jet) %parula
    colorbar();
    title('Standart filter');
end