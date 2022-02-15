function gui_preview(sample, koef, Lowpass, Hipass, THRESHOLD)
    imOriginal=Rload(sample,1,koef);
    imFilter= sgauss(imOriginal, Lowpass,Hipass);
    imThreshold = (imFilter - THRESHOLD) .* (imFilter > THRESHOLD);
    image(vol2proj(imThreshold),'CDataMapping','scaled');
    axis image
    colormap(jet) %parula
    colorbar();
    title('Common filters');
end