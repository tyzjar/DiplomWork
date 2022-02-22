function gui_thresholdPreview(sample, koef, THRESHOLD)
    imOriginal=Rload(sample,1,koef);
    imThreshold = (imOriginal - THRESHOLD) .* (imOriginal > THRESHOLD);
    figure
    image(vol2proj(imThreshold),'CDataMapping','scaled');
    axis image
    colormap(jet) %parula
    colorbar();
    title('Threshold');
end